import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SuperpowerService } from '../../services/superpower.service';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-superpower-form',
  template: `
    <h2>{{ isEditMode ? 'Editar' : 'Criar' }} Superpoder</h2>
    
    <div *ngIf="loading" class="d-flex justify-content-center my-4">
      <div class="spinner-border" role="status">
        <span class="visually-hidden">Carregando...</span>
      </div>
    </div>
    
    <form *ngIf="!loading" [formGroup]="superpowerForm" (ngSubmit)="onSubmit()">
      <div class="mb-3">
        <label for="name" class="form-label">Nome</label>
        <input type="text" class="form-control" id="name" formControlName="name">
        <div *ngIf="superpowerForm.get('name')?.invalid && superpowerForm.get('name')?.touched" class="text-danger">
          Nome é obrigatório
        </div>
      </div>
      
      <div class="mb-3">
        <label for="description" class="form-label">Descrição</label>
        <textarea class="form-control" id="description" rows="3" formControlName="description"></textarea>
        <div *ngIf="superpowerForm.get('description')?.invalid && superpowerForm.get('description')?.touched" class="text-danger">
          Descrição é obrigatória
        </div>
      </div>
      
      <div class="d-flex gap-2">
        <button type="submit" class="btn btn-primary" [disabled]="superpowerForm.invalid">Salvar</button>
        <a routerLink="/superpowers" class="btn btn-secondary">Cancelar</a>
      </div>
    </form>
  `,
  styles: []
})
export class SuperpowerFormComponent implements OnInit {
  superpowerForm: FormGroup;
  isEditMode = false;
  superpowerId: number | null = null;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private superpowerService: SuperpowerService,
    private route: ActivatedRoute,
    private router: Router,
    private notificationService: NotificationService
  ) {
    this.superpowerForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.superpowerId = +id;
      this.loadSuperpower(this.superpowerId);
    }
  }

  loadSuperpower(id: number): void {
    this.loading = true;
    this.superpowerService.getSuperpower(id)
      .subscribe({
        next: (superpower) => {
          this.superpowerForm.patchValue({
            name: superpower.name,
            description: superpower.description
          });
          this.loading = false;
        },
        error: () => {
          this.loading = false;
          setTimeout(() => {
            this.router.navigate(['/superpowers']);
          }, 3000);
        },        
        complete: () => {
          this.loading = false;
        }
      });
  }

  onSubmit(): void {
    if (this.superpowerForm.invalid) {
      return;
    }

    if (this.isEditMode && this.superpowerId) {
      this.superpowerService.updateSuperpower(this.superpowerId, this.superpowerForm.value)
        .subscribe({
          next: () => this.router.navigate(['/superpowers']),
          error: () => {
          }
        });
    } else {
      this.superpowerService.createSuperpower(this.superpowerForm.value)
        .subscribe({
          next: () => this.router.navigate(['/superpowers']),
          error: () => {
          }
        });
    }
  }
}