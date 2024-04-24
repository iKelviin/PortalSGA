import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './Core/Pages/layout/layout.component';
import { HomeEmpresaComponent } from './Pages/Cadastro/Empresa/home-empresa/home-empresa.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      {
          path:'empresas',
          component: HomeEmpresaComponent
      }
  ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
