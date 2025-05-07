import { Superpower, CreateSuperpower } from './superpower.model';

export interface Hero {
  id: number;
  name: string;
  heroName: string;
  description: string;
  dateOfBirth: Date;
  height: number;
  weight: number;
  superpowers: Superpower[];
}

export interface CreateHero {
  name: string;
  heroName: string;
  description: string;
  dateOfBirth: Date;
  height: number;
  weight: number;
  superpowerIds: number[];
  newSuperpowers: CreateSuperpower[];
}

export interface UpdateHero {
  name: string;
  heroName: string;
  description: string;
  dateOfBirth: Date;
  height: number;
  weight: number;
  superpowerIds: number[];
  newSuperpowers: CreateSuperpower[];
}