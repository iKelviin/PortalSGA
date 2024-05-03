import { NgModule } from '@angular/core';

//Components do Angular Material
import {MatSortModule} from '@angular/material/sort';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatButtonModule} from "@angular/material/button"
import {MatIconModule} from "@angular/material/icon"
import {MatMenuModule} from "@angular/material/menu"
import {MatToolbarModule} from "@angular/material/toolbar"
import {MatCardModule} from "@angular/material/card"
import { MatInputModule } from '@angular/material/input';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSliderModule } from '@angular/material/slider';
import {  MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import {MatDividerModule} from '@angular/material/divider';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { MatTooltipModule } from '@angular/material/tooltip';
import {MatTreeModule} from '@angular/material/tree';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import { MatSlideToggleModule } from "@angular/material/slide-toggle";
import {MatGridListModule} from '@angular/material/grid-list';


@NgModule({

  exports:[
    MatProgressBarModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatCardModule,
    MatToolbarModule,
    MatSidenavModule,
    MatListModule,
    MatMenuModule,
    MatDialogModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatFormFieldModule,
    MatSnackBarModule,
    MatSelectModule,
    MatCheckboxModule,
    MatNativeDateModule,
    MatDatepickerModule,
    MatRadioModule,
    MatSliderModule,
    CommonModule,
    BrowserAnimationsModule,
    MatProgressSpinnerModule,
    MatDividerModule,
    MatTooltipModule,
    MatTreeModule,
    MatSlideToggleModule,
    MatGridListModule
    ],

})
export class AppMaterialModule { }
