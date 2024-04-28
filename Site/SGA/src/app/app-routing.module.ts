import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './Core/Pages/layout/layout.component';
import { HomeEmpresaComponent } from './Pages/Cadastro/Empresa/home-empresa/home-empresa.component';
import { HomeDepartamentoComponent } from './Pages/Cadastro/Departamento/home-departamento/home-departamento.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      {
          path:'empresas',
          component: HomeEmpresaComponent
      },
      {
        path:'departamentos',
        component: HomeDepartamentoComponent
      }
  ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
