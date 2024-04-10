import { Component } from '@angular/core';
import { BodyComponent } from '../../Components/body/body.component';
import { CommonModule } from '@angular/common';
import { SidebarComponent } from '../../Components/sidebar/sidebar.component';
import { RouterOutlet } from '@angular/router';

interface SideBarToggle{
  screenWidth: number;
  collapsed: boolean;
}

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [CommonModule,SidebarComponent,BodyComponent,RouterOutlet],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.scss'
})
export class LayoutComponent {

  isSideBarCollapsed = false;
  screenWidth= 0;

  onToggleSideBar(data: SideBarToggle): void {
    this.screenWidth = data.screenWidth;
    this.isSideBarCollapsed = data.collapsed;
  }
}
