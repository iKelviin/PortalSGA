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
import { NgxViacepModule } from "@brunoc/ngx-viacep"; // Importando o módulo


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
