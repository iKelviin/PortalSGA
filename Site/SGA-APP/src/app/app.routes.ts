import { Routes } from '@angular/router';
import { LayoutComponent } from './Core/Pages/layout/layout.component';
import { HomeEmpresaComponent } from './Pages/Cadastro/Empresa/home-empresa/home-empresa.component';

export const routes: Routes = [
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
