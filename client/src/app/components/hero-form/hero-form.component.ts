import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { HeroService } from '../../services/hero.service';
import { SuperpowerService } from '../../services/superpower.service';
import { Superpower, CreateSuperpower } from '../../models/superpower.model';

interface SuperpowerOption {
  id?: number;
  name: string;
  description: string;
  isNew?: boolean;
}

@Component({
  selector: 'app-hero-form',
  template: `
    <h2>{{ isEditMode ? 'Editar' : 'Criar' }} Herói</h2>
    
    <form [formGroup]="heroForm" (ngSubmit)="onSubmit()">
      <div class="mb-3">
        <label for="name" class="form-label">Nome *</label>
        <input type="text" class="form-control" id="name" formControlName="name">
        <div *ngIf="heroForm.get('name')?.invalid && heroForm.get('name')?.touched" class="text-danger">
          <div *ngIf="heroForm.get('name')?.errors?.['required']">Nome é obrigatório</div>
          <div *ngIf="heroForm.get('name')?.errors?.['duplicateName']">Já existe um herói com este nome</div>
        </div>
      </div>
      
      <div class="mb-3">
        <label for="heroName" class="form-label">Nome de Herói *</label>
        <input type="text" class="form-control" id="heroName" formControlName="heroName">
        <div *ngIf="heroForm.get('heroName')?.invalid && heroForm.get('heroName')?.touched" class="text-danger">
          Nome de herói é obrigatório
        </div>
      </div>
      
      <div class="mb-3">
        <label for="description" class="form-label">Descrição</label>
        <textarea class="form-control" id="description" formControlName="description" rows="3"></textarea>
      </div>
      
      <div class="mb-3">
        <label for="dateOfBirth" class="form-label">Data de Nascimento *</label>
        <input type="date" class="form-control" id="dateOfBirth" formControlName="dateOfBirth">
        <div *ngIf="heroForm.get('dateOfBirth')?.invalid && heroForm.get('dateOfBirth')?.touched" class="text-danger">
          Data de nascimento é obrigatória
        </div>
      </div>
      
      <div class="mb-3">
        <label for="height" class="form-label">Altura (cm) *</label>
        <input type="number" class="form-control" id="height" formControlName="height">
        <div *ngIf="heroForm.get('height')?.invalid && heroForm.get('height')?.touched" min="0" max="200" class="text-danger">
          Altura é obrigatória e deve ser um número positivo menor que 200
        </div>
      </div>
      
      <div class="mb-3">
        <label for="weight" class="form-label">Peso (kg) *</label>
        <input type="number" class="form-control" id="weight" formControlName="weight" min="0" max="500">
        <div *ngIf="heroForm.get('weight')?.invalid && heroForm.get('weight')?.touched" class="text-danger">
          Peso é obrigatório e deve ser um número positivo menor que 500
        </div>
      </div>
      
      <div class="mb-3">
        <label class="form-label">Superpoderes</label>
        <ng-select
          [items]="availableSuperPowerOptions"
          [addTag]="addTagFn"
          [multiple]="true"
          [hideSelected]="true"
          [trackByFn]="trackByFn"
          bindLabel="name"
          placeholder="Selecione ou crie superpoderes"
          [(ngModel)]="selectedSuperPowerOptions"
          [ngModelOptions]="{standalone: true}"
          class="custom-select"
          appendTo="body"
        >
          <ng-template ng-label-tmp let-item="item" let-clear="clear">
            <span class="ng-value-label">
              {{ item.name }}
              <span *ngIf="item.isNew" class="badge bg-success ms-1">Novo</span>
            </span>
            <span class="ng-value-icon right" (click)="clear(item)" aria-hidden="true">×</span>
          </ng-template>
          
          <ng-template ng-option-tmp let-item="item">
            <div>
              <strong>{{ item.name }}</strong>
              <small *ngIf="item.description" class="text-muted d-block">{{ item.description }}</small>
              <span *ngIf="item.isNew" class="badge bg-success float-end">Novo</span>
            </div>
          </ng-template>
        </ng-select>
        <small class="form-text text-muted">
          Digite para pesquisar superpoderes existentes ou criar novos digitando e pressionando Enter
        </small>
      </div>
      
      <div class="d-flex gap-2 mt-3">
        <button type="submit" class="btn btn-primary" [disabled]="heroForm.invalid">Salvar</button>
        <a routerLink="/heroes" class="btn btn-secondary">Cancelar</a>
      </div>
    </form>
  `,
  styles: []
})
export class HeroFormComponent implements OnInit {
  heroForm: FormGroup;
  isEditMode = false;
  heroId: number | null = null;
  
  availableSuperpowers: Superpower[] = [];
  
  availableSuperPowerOptions: SuperpowerOption[] = [];
  selectedSuperPowerOptions: SuperpowerOption[] = [];

  constructor(
    private fb: FormBuilder,
    private heroService: HeroService,
    private superpowerService: SuperpowerService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.heroForm = this.fb.group({
      name: ['', Validators.required],
      heroName: ['', Validators.required],
      description: [''],
      dateOfBirth: ['', Validators.required],
      height: ['', [Validators.required, Validators.min(0), Validators.max(200)]],
      weight: ['', [Validators.required, Validators.min(0), Validators.max(500)]]
    });
  }

  ngOnInit(): void {
    this.loadSuperpowers();
    
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.heroId = +id;
      this.loadHero(this.heroId);
    }
  }

  loadSuperpowers(): void {
    this.superpowerService.getSuperpowers()
      .subscribe(superpowers => {
        this.availableSuperpowers = superpowers;
        this.availableSuperPowerOptions = superpowers.map(sp => ({
          id: sp.id,
          name: sp.name,
          description: sp.description
        }));
      });
  }

  loadHero(id: number): void {
    this.heroService.getHero(id)
      .subscribe({
        next: (hero) => {
          this.heroForm.patchValue({
            name: hero.name,
            heroName: hero.heroName,
            description: hero.description,
            dateOfBirth: new Date(hero.dateOfBirth).toISOString().split('T')[0],
            height: hero.height,
            weight: hero.weight
          });
          
          // Carrega superpoderes já existentes
          this.selectedSuperPowerOptions = hero.superpowers.map(sp => ({
            id: sp.id,
            name: sp.name,
            description: sp.description
          }));
        },
        error: () => {
          setTimeout(() => {
            this.router.navigate(['/heroes']);
          }, 3000);
        }
      });
  }

  // Função para adicionar um novo superpoder (tag)
  addTagFn(name: string): SuperpowerOption {
    return {
      name: name,
      description: '',
      isNew: true
    };
  }

  trackByFn(item: SuperpowerOption) {
    return item.id ? item.id : item.name;
  }

  onSubmit(): void {
    if (this.heroForm.invalid) {
      return;
    }

    const existingSuperpowers = this.selectedSuperPowerOptions
      .filter(sp => !sp.isNew)
      .map(sp => sp.id as number);

    const newSuperpowers = this.selectedSuperPowerOptions
      .filter(sp => sp.isNew)
      .map(sp => ({
        name: sp.name,
        description: sp.description || ''
      }));

    const heroData = {
      ...this.heroForm.value,
      superpowerIds: existingSuperpowers,
      newSuperpowers: newSuperpowers
    };

    if (this.isEditMode && this.heroId) {
      this.heroService.updateHero(this.heroId, heroData)
        .subscribe({
          next: () => this.router.navigate(['/heroes']),
          error: (error) => {
            if (error.error && error.error.includes('existe')) {
              this.heroForm.get('name')?.setErrors({ duplicateName: true });
            }
          }
        });
    } else {
      this.heroService.createHero(heroData)
        .subscribe({
          next: () => this.router.navigate(['/heroes']),
          error: (error) => {
            if (error.error && error.error.includes('existe')) {
              this.heroForm.get('name')?.setErrors({ duplicateName: true });
            }
          }
        });
    }
  }
}