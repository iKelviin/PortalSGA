import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './Core/Pages/layout/layout.component';
import { HomeEmpresaComponent } from './Pages/Cadastro/Empresa/home-empresa/home-empresa.component';
import { HomeDepartamentoComponent } from './Pages/Cadastro/Departamento/home-departamento/home-departamento.component';
import { HomeCargoComponent } from './Pages/Cadastro/Cargo/home-cargo/home-cargo.component';
import { HomeGerarComponent } from './Pages/Solicitações/GerarSolicitacao/home-gerar/home-gerar.component';

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
        path:'departamentos/:idEmpresa',
        component: HomeDepartamentoComponent
      },
      {
        path:'cargos',
        component: HomeCargoComponent
      },
      {
        path:'empresas/:idEmpresa/departamentos/:idDepartamento/cargos',
        component: HomeCargoComponent
      },
      {
        path:'gerar',
        component: HomeGerarComponent
      }
  ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
