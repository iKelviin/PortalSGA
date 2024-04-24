import { Component, EventEmitter, HostListener, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ISidebarData } from './helper';
import { sidebarData } from './sidebar-menu';
import { animate, style, transition, trigger } from '@angular/animations';

interface SideBarToggle{
  screenWidth: number;
  collapsed: boolean;
}

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss',
  animations: [
    trigger('fadeInOut', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateX(20px)' }),
        animate('500ms ease-in-out', style({ opacity: 1, transform: 'translateX(0)' }))
      ]),
      transition(':leave', [
        style({ opacity: 1, transform: 'translateX(0)' }),
        animate('100ms ease-in-out', style({ opacity: 0, transform: 'translateX(-20px)' }))
      ])
    ])
  ]
})
export class SidebarComponent implements OnInit{

  @Output() onToggleSideBar: EventEmitter<SideBarToggle> = new EventEmitter();
  collapsed = false;
  screenWidth = 0;
  sideData = sidebarData;
  multiple: boolean = false;


  @HostListener('window:resize', ['$event'])
  onResize(event: any){
    this.screenWidth = window.innerWidth;
    if(this.screenWidth <= 768){
      this.collapsed = false;
      this.onToggleSideBar.emit({collapsed: this.collapsed, screenWidth: this.screenWidth});
    }
  }


  constructor(public router: Router) {}


  ngOnInit(): void {
    this.screenWidth = window.innerWidth;
  }

  toggleCollapse(): void{
    this.collapsed = !this.collapsed;
    this.onToggleSideBar.emit({collapsed: this.collapsed, screenWidth: this.screenWidth});
  }

  closeSideBar(): void {
    this.collapsed = false;
    this.onToggleSideBar.emit({collapsed: this.collapsed, screenWidth: this.screenWidth});
  }

  handleClick(item: ISidebarData): void {
    this.shrinkItems(item);
    item.expanded = !item.expanded
  }

  getActiveClass(data: ISidebarData): string {
    return this.router.url.includes(data.routeLink)? 'active':'';
  }

  shrinkItems(item: ISidebarData): void {
    if(!this.multiple){
      for(let modelItem of this.sideData){
        if(item !== modelItem && modelItem.expanded){
          modelItem.expanded = false;
        }
      }
    }
  }


}
