import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Superpower, CreateSuperpower, UpdateSuperpower } from '../models/superpower.model';
import { environment } from '../../environments/environment';
import { NotificationService } from './notification.service';

@Injectable({
  providedIn: 'root'
})
export class SuperpowerService {
  private apiUrl = `${environment.apiUrl}/superpowers`;

  constructor(
    private http: HttpClient,
    private notificationService: NotificationService
  ) { }

  getSuperpowers(): Observable<Superpower[]> {
    return this.http.get<Superpower[]>(this.apiUrl).pipe(
      catchError(error => {
        if (error.status === 204) {
          this.notificationService.info('Nenhum superpoder encontrado');
          return of([]);
        }
        return this.handleError('Falha ao carregar superpoderes', error);
      })
    );
  }

  getSuperpower(id: number): Observable<Superpower> {
    return this.http.get<Superpower>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.notificationService.info(`Detalhes do superpoder carregados com sucesso`)),
      catchError(error => this.handleError(`Falha ao carregar superpoder com ID ${id}`, error))
    );
  }

  createSuperpower(superpower: CreateSuperpower): Observable<Superpower> {
    return this.http.post<Superpower>(this.apiUrl, superpower).pipe(
      tap(() => this.notificationService.success('Superpoder criado com sucesso')),
      catchError(error => this.handleError('Falha ao criar superpoder', error))
    );
  }

  updateSuperpower(id: number, superpower: UpdateSuperpower): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, superpower).pipe(
      tap(() => this.notificationService.success('Superpoder atualizado com sucesso')),
      catchError(error => this.handleError(`Falha ao atualizar superpoder com ID ${id}`, error))
    );
  }

  deleteSuperpower(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      tap(() => this.notificationService.success('Superpoder excluído com sucesso')),
      catchError(error => this.handleError(`Falha ao excluir superpoder com ID ${id}`, error))
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
        this.notificationService.error("Nenhum registro encontrado");
      } else {
        this.notificationService.error(`${message}: Erro na execução.`);
      }
    }
    
    return throwError(() => error);
  }
}