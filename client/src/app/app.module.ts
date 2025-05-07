import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { NgSelectModule } from '@ng-select/ng-select';

import { AppComponent } from './app.component';
import { HeroListComponent } from './components/hero-list/hero-list.component';
import { HeroDetailComponent } from './components/hero-detail/hero-detail.component';
import { HeroFormComponent } from './components/hero-form/hero-form.component';
import { SuperpowerListComponent } from './components/superpower-list/superpower-list.component';
import { SuperpowerFormComponent } from './components/superpower-form/superpower-form.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { NotificationsComponent } from './components/notifications/notifications.component';

const routes: Routes = [
  { path: '', redirectTo: '/heroes', pathMatch: 'full' },
  { path: 'heroes', component: HeroListComponent },
  { path: 'heroes/new', component: HeroFormComponent },
  { path: 'heroes/:id', component: HeroDetailComponent },
  { path: 'heroes/:id/edit', component: HeroFormComponent },
  { path: 'superpowers', component: SuperpowerListComponent },
  { path: 'superpowers/new', component: SuperpowerFormComponent },
  { path: 'superpowers/:id/edit', component: SuperpowerFormComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    HeroListComponent,
    HeroDetailComponent,
    HeroFormComponent,
    SuperpowerListComponent,
    SuperpowerFormComponent,
    NavbarComponent,
    NotificationsComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgSelectModule,
    RouterModule.forRoot(routes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }