import { Component, OnInit } from '@angular/core';
import { Superpower } from '../../models/superpower.model';
import { SuperpowerService } from '../../services/superpower.service';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-superpower-list',
  template: `
    <div class="row mb-3">
      <div class="col">
        <h2>Superpoderes</h2>
      </div>
      <div class="col-auto">
        <a routerLink="/superpowers/new" class="btn btn-primary">Adicionar Superpoder</a>
      </div>
    </div>

    <div class="alert alert-info" *ngIf="superpowers.length === 0 && !loading">
      Nenhum superpoder encontrado. Adicione um novo superpoder para começar.
    </div>

    <div *ngIf="loading" class="d-flex justify-content-center">
      <div class="spinner-border" role="status">
        <span class="visually-hidden">Carregando...</span>
      </div>
    </div>

    <div class="table-responsive" *ngIf="superpowers.length > 0">
      <table class="table table-striped">
        <thead>
          <tr>
            <th>ID</th>
            <th>Nome</th>
            <th>Descrição</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          <tr *ngFor="let superpower of superpowers">
            <td>{{ superpower.id }}</td>
            <td>{{ superpower.name }}</td>
            <td>{{ superpower.description }}</td>
            <td>
              <div class="btn-group btn-group-sm">
                <a [routerLink]="['/superpowers', superpower.id, 'edit']" class="btn btn-warning">Editar</a>
                <button (click)="deleteSuperpower(superpower.id)" class="btn btn-danger">Excluir</button>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  `,
  styles: []
})
export class SuperpowerListComponent implements OnInit {
  superpowers: Superpower[] = [];
  loading = true;

  constructor(
    private superpowerService: SuperpowerService,
    private notificationService: NotificationService
  ) { }

  ngOnInit(): void {
    this.loadSuperpowers();
  }

  loadSuperpowers(): void {
    this.loading = true;
    this.superpowerService.getSuperpowers()
      .subscribe({
        next: (superpowers) => {
          this.superpowers = superpowers;
          this.loading = false;
        },
        error: () => {
          this.loading = false;
        }
      });
  }

  deleteSuperpower(id: number): void {
    if (confirm('Tem certeza que deseja excluir este superpoder?')) {
      this.superpowerService.deleteSuperpower(id)
        .subscribe({
          next: () => {
            this.superpowers = this.superpowers.filter(s => s.id !== id);
          }
        });
    }
  }
}