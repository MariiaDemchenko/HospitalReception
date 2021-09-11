import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

export const routes: Routes = [];

@NgModule({
  imports: [BrowserModule, RouterModule.forRoot(routes), FormsModule],
  exports: [RouterModule],
  providers: []
})

export class AppRoutingModule { }
