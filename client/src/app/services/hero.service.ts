import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Hero, CreateHero, UpdateHero } from '../models/hero.model';
import { environment } from '../../environments/environment';
import { NotificationService } from './notification.service';

@Injectable({
  providedIn: 'root'
})
export class HeroService {
  private apiUrl = `${environment.apiUrl}/heroes`;

  constructor(
    private http: HttpClient,
    private notificationService: NotificationService
  ) { }

  getHeroes(): Observable<Hero[]> {
    return this.http.get<Hero[]>(this.apiUrl).pipe(
      catchError(error => {
        if (error.status === 204) {
          this.notificationService.info('Nenhum herói encontrado');
          return of([]);
        }
        return this.handleError('Falha ao caregar heróis ', error);
      })
    );
  }

  getHero(id: number): Observable<Hero> {
    return this.http.get<Hero>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.notificationService.info(`Detalhes do herói carregados com sucesso`)),
      catchError(error => this.handleError(`Falha ao carregar herói`, error))
    );
  }

  createHero(hero: CreateHero): Observable<Hero> {
    return this.http.post<Hero>(this.apiUrl, hero).pipe(
      tap(() => this.notificationService.success('Herói criado com sucesso')),
      catchError(error => this.handleError('Falha ao cadastrar herói ', error))
    );
  }

  updateHero(id: number, hero: UpdateHero): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, hero).pipe(
      tap(() => this.notificationService.success('Herói atualizado com sucesso')),
      catchError(error => this.handleError(`Falha ao atualizar herói`, error))
    );
  }

  deleteHero(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.notificationService.success('Herói excluído com sucesso')),
      catchError(error => this.handleError(`Falha ao remover herói`, error))
    );
  }

  private handleError(message: string, error: HttpErrorResponse): Observable<never> {
    console.error('Ocorreu um erro:', error);
    
    if (error.error instanceof ErrorEvent) {
      this.notificationService.error(`${message}: ${error.error.message}`);
    } else {
      if (error.status === 404 && typeof error.error === 'string') {
        this.notificationService.error(error.error);
      } else if (error.status === 204) {
        this.notificationService.info('Nenhum registro encontrado');
      } else {
        this.notificationService.error(`${message}: Erro na execução.`);
      }
    }
    
    return throwError(() => error);
  }
}