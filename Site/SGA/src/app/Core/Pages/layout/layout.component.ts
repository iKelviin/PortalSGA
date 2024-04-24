import { Component } from '@angular/core';

interface SideBarToggle{
  screenWidth: number;
  collapsed: boolean;
}


@Component({
  selector: 'app-layout',
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
