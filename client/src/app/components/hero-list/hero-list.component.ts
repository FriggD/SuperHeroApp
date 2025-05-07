import { Component, OnInit } from '@angular/core';
import { Hero } from '../../models/hero.model';
import { HeroService } from '../../services/hero.service';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-hero-list',
  template: `
    <div class="row mb-3">
      <div class="col">
        <h2>Heróis</h2>
      </div>
      <div class="col-auto">
        <a routerLink="/heroes/new" class="btn btn-primary">Adicionar Herói</a>
      </div>
    </div>
    <div class="alert alert-info" *ngIf="heroes.length === 0 && !loading">
      Nenhum herói encontrado. Adicione um novo herói para começar.
    </div>

    <div *ngIf="loading" class="d-flex justify-content-center">
      <div class="spinner-border" role="status">
        <span class="visually-hidden">Carregando...</span>
      </div>
    </div>

    <div class="table-responsive" *ngIf="heroes.length > 0">
      <table class="table table-striped">
        <thead>
          <tr>
            <th>ID</th>
            <th>Nome</th>
            <th>Nome de Herói</th>
            <th>Descrição</th>
            <th>Data de Nascimento</th>
            <th>Altura</th>
            <th>Peso</th>
            <th>Superpoderes</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          <tr *ngFor="let hero of heroes">
            <td>{{ hero.id }}</td>
            <td>{{ hero.name }}</td>
            <td>{{ hero.heroName }}</td>
            <td>{{ hero.description | slice:0:50 }}{{ hero.description.length > 50 ? '...' : '' }}</td>
            <td>{{ hero.dateOfBirth | date }}</td>
            <td>{{ hero.height }} cm</td>
            <td>{{ hero.weight }} kg</td>
            <td>
              <span *ngFor="let power of hero.superpowers; let last = last">
                {{ power.name }}{{ !last ? ', ' : '' }}
              </span>
              <span *ngIf="hero.superpowers.length === 0" class="text-muted">Nenhum</span>
            </td>
            <td>
              <div class="btn-group btn-group-sm">
                <a [routerLink]="['/heroes', hero.id]" class="btn btn-info">Ver</a>
                <a [routerLink]="['/heroes', hero.id, 'edit']" class="btn btn-warning">Editar</a>
                <button (click)="deleteHero(hero.id)" class="btn btn-danger">Excluir</button>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  `,
  styles: []
})
export class HeroListComponent implements OnInit {
  heroes: Hero[] = [];
  loading = true;

  constructor(
    private heroService: HeroService,
    private notificationService: NotificationService
  ) { }

  ngOnInit(): void {
    this.loadHeroes();
  }

  loadHeroes(): void {
    this.loading = false;
    this.heroService.getHeroes()
      .subscribe({
        next: (heroes) => {
          this.heroes = heroes;
          this.loading = false;
        },
        error: () => {
          this.loading = false;
        },
        complete: () => {
          this.loading = false;
        }
      });
  }

  deleteHero(id: number): void {
    if (confirm('Tem certeza que deseja excluir este herói?')) {
      this.heroService.deleteHero(id)
        .subscribe({
          next: () => {
            this.heroes = this.heroes.filter(h => h.id !== id);
          }
        });
    }
  }
}