import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { SidebarComponent } from './Core/Components/sidebar/sidebar.component';
import { BodyComponent } from './Core/Components/body/body.component';
import { trigger, state, style, transition, animate } from '@angular/animations';

interface SideBarToggle{
  screenWidth: number;
  collapsed: boolean;
}

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
    imports: [CommonModule, RouterOutlet, HttpClientModule, SidebarComponent, BodyComponent],
    providers: [
      HttpClientModule
    ],
    animations: [
      trigger('transformMenu', [
        state('void', style({ opacity: 0, transform: 'scale(0.8)' })),
        transition('void <=> *', [
          animate(200)
        ])
      ])
    ]
})

export class AppComponent {
  title = 'SGA-APP';

  isSideBarCollapsed = false;
  screenWidth= 0;

  onToggleSideBar(data: SideBarToggle): void {
    this.screenWidth = data.screenWidth;
    this.isSideBarCollapsed = data.collapsed;
  }
}
