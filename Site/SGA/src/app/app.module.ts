import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BodyComponent } from './Core/Components/body/body.component';
import { LayoutComponent } from './Core/Pages/layout/layout.component';
import { AppMaterialModule } from './Material/app-material/app-material.module';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { SidebarComponent } from './Core/Components/sidebar/sidebar.component';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { SublevelMenuComponent } from './Core/Components/sidebar/sublevel-menu.component';
import { SublevelBarComponent } from './Core/Components/sidebar/sublevel-bar.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AlertService } from './Shared/Services/alert.service';
import { EmpresaService } from './Services/empresa.service';
import { HomeEmpresaComponent } from './Pages/Cadastro/Empresa/home-empresa/home-empresa.component';
import { ModalEmpresaComponent } from './Pages/Cadastro/Empresa/modal-empresa/modal-empresa.component';
import { provideHotToastConfig } from '@ngneat/hot-toast';
import { InputSearchComponent } from './Shared/Components/input-search/input-search.component';
import { InputSelectComponent } from './Shared/Components/input-select/input-select.component';
import { MatTableExporterModule } from 'mat-table-exporter';
import { NgxViacepModule } from "@brunoc/ngx-viacep";
import { HomeDepartamentoComponent } from './Pages/Cadastro/Departamento/home-departamento/home-departamento.component';
import { ModalDepartamentoComponent } from './Pages/Cadastro/Departamento/modal-departamento/modal-departamento.component';
import { HomeCargoComponent } from './Pages/Cadastro/Cargo/home-cargo/home-cargo.component';
import { ModalCargoComponent } from './Pages/Cadastro/Cargo/modal-cargo/modal-cargo.component';
import { HomeGerarComponent } from './Pages/Solicitações/GerarSolicitacao/home-gerar/home-gerar.component';
import { HeaderComponent } from './Core/Components/header/header.component'; // Importando o módulo


@NgModule({
  declarations: [
    AppComponent,
    BodyComponent,
    LayoutComponent,
    SidebarComponent,
    SublevelBarComponent,
    SublevelMenuComponent,
    HomeEmpresaComponent,
    ModalEmpresaComponent,
    InputSearchComponent,
    InputSelectComponent,
    HomeDepartamentoComponent,
    ModalDepartamentoComponent,
    HomeCargoComponent,
    ModalCargoComponent,
    HomeGerarComponent,
    HeaderComponent,    
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AppMaterialModule,
    RouterModule,
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    MatTableExporterModule,
    NgxViacepModule
  ],
  exports:[
    AppMaterialModule,
    CommonModule,
    RouterModule,
    AppRoutingModule,
    FormsModule,
    BrowserAnimationsModule,
    MatTableExporterModule
  ],
  providers: [
    provideClientHydration(),
    provideAnimationsAsync(),
    AlertService,
    EmpresaService,
    provideHotToastConfig(
      {
        stacking: "depth",
        visibleToasts: 5,
        reverseOrder: true
      }
    ),
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
