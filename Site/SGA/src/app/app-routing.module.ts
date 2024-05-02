import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './Core/Pages/layout/layout.component';
import { HomeEmpresaComponent } from './Pages/Cadastro/Empresa/home-empresa/home-empresa.component';
import { HomeDepartamentoComponent } from './Pages/Cadastro/Departamento/home-departamento/home-departamento.component';
import { HomeCargoComponent } from './Pages/Cadastro/Cargo/home-cargo/home-cargo.component';

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
      },
      {
        path:'cargos',
        component: HomeCargoComponent
      }
  ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
