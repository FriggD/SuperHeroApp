import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Hero } from '../../models/hero.model';
import { HeroService } from '../../services/hero.service';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-hero-detail',
  template: `
    <div *ngIf="hero">
      <h2>Detalhes de {{ hero.heroName }}</h2>
      
      <div class="card mb-4">
        <div class="card-body">
          <h3 class="card-title">{{ hero.name }} <small class="text-muted">({{ hero.heroName }})</small></h3>
          <div class="row">
            <div class="col-md-6">
              <p><strong>Data de Nascimento:</strong> {{ hero.dateOfBirth | date }}</p>
              <p><strong>Altura:</strong> {{ hero.height }} cm</p>
              <p><strong>Peso:</strong> {{ hero.weight }} kg</p>
              <div *ngIf="hero.description" class="mt-3">
                <h4>Descrição</h4>
                <p>{{ hero.description }}</p>
              </div>
            </div>
            <div class="col-md-6">
              <h4>Superpoderes</h4>
              <ul class="list-group">
                <li class="list-group-item" *ngFor="let power of hero.superpowers">
                  <strong>{{ power.name }}</strong>
                  <p *ngIf="power.description">{{ power.description }}</p>
                </li>
                <li class="list-group-item text-muted" *ngIf="hero.superpowers.length === 0">
                  Nenhum superpoder atribuído
                </li>
              </ul>
            </div>
          </div>
        </div>
        <div class="card-footer">
          <div class="btn-group">
            <a [routerLink]="['/heroes', hero.id, 'edit']" class="btn btn-primary">Editar</a>
            <a routerLink="/heroes" class="btn btn-secondary">Voltar para Lista</a>
          </div>
        </div>
      </div>
    </div>
    
    <div *ngIf="!hero && loading" class="d-flex justify-content-center">
      <div class="spinner-border" role="status">
        <span class="visually-hidden">Carregando...</span>
      </div>
    </div>
    
    <div *ngIf="!hero && !loading" class="alert alert-danger">
      <h4>Herói Não Encontrado</h4>
      <p>O herói que você está procurando não foi encontrado.</p>
      <a routerLink="/heroes" class="btn btn-primary">Voltar para Lista de Heróis</a>
    </div>
  `,
  styles: []
})
export class HeroDetailComponent implements OnInit {
  hero: Hero | null = null;
  loading = true;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private heroService: HeroService,
    private notificationService: NotificationService
  ) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.heroService.getHero(id)
      .subscribe({
        next: (hero) => {
          this.hero = hero;
          this.loading = false;
        },
        error: (err) => {
          this.loading = false;
        }
      });
  }
}