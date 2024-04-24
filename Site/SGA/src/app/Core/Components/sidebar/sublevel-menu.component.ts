import { Component, Input, OnInit } from '@angular/core';
import { ISidebarData, fadeInOut } from './helper';
import { Router } from '@angular/router';
import { animate, state, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-sublevel-menu',
  styleUrls: ['./sidebar.component.scss'],
  template:`
          <ul *ngIf="collapsed && data.items && data.items.length > 0"
            [@submenu]="expanded ? {value: 'visible',
               params: {transitionParams: '500ms cubic-bezier(0.86, 0, 0.07, 1)',height: '*'}}
            : {value: 'hidden',
               params: {transitionParams: '500ms cubic-bezier(0.86, 0, 0.07, 1)',height: '0'}}"
            class="sublevel-bar">

            <li *ngFor="let item of data.items" class="sublevel-bar-item">
                <a class="sublevel-bar-link"
                (click)="handleClick(item)"
                *ngIf="item.items && item.items.length > 0"
                [ngClass]="getActiveClass(item)"
                >
                <mat-icon class="sublevel-link-icon">{{item.icon}}</mat-icon>
                  <span class="sublevel-link-text" @fadeInOut *ngIf="collapsed">
                    {{item.label}}
                  </span>
                <mat-icon *ngIf="item.items && collapsed" class="menu-collapse-icon">
                    {{!item.expanded? 'chevron_right': 'expand_more'}}
                </mat-icon>
              </a>
              <a class="sublevel-bar-link"
                *ngIf="!item.items || (item.items && item.items.length === 0)"
                [routerLink]="[item.routeLink]"
                routerLinkActive="active-sublevel"
                [routerLinkActiveOptions]="{exact: true}"
              >
                <mat-icon class="sublevel-link-icon">{{item.icon}}</mat-icon>
                <span class="sublevel-link-text" @fadeInOut
                *ngIf="collapsed">{{item.label}}</span>
              </a>
              <div *ngIf="item.items && item.items.length > 0">
                <app-sublevel-menu
                 [data]="item"
                 [collapsed]="collapsed"
                 [multiple]="multiple"
                 [expanded]="item.expanded"
                ></app-sublevel-menu>
              </div>
            </li>
          </ul>
  `,
  animations: [
    fadeInOut,
    trigger('submenu',[
      state('hidden',style({
        height: '0',
        overflow: 'hidden',
      })),
      state('visible',style({
        height:'*'
      })),
      transition('visible <=> hidden', [style({overflow: 'hidden'}),
        animate('{{transitionParams}}')]),
      transition('void => *',animate(0))
    ])
  ]
})
export class SublevelMenuComponent implements OnInit{

  @Input() data: ISidebarData = {
    routeLink: '',
    icon: '',
    label: '',
    //sublevel: 0,
    items: []
  }

  @Input() collapsed = false;
  @Input() animating: boolean | undefined;
  @Input() expanded: boolean | undefined;
  @Input() multiple: boolean = false;


  ngOnInit(): void {
  }

  constructor(public router: Router) {}



  /*isExpandable(item: ISidebarData): boolean {
    if(item.items && item.items.length > 0){
      return true;
    }
    else{
      return false;
    }
  }*/

/*log(item: ISidebarData){
  return console.log(item)
}*/

  handleClick(item: any): void {
    if(!this.multiple){
      if(this.data.items && this.data.items.length > 0){
        for(let modelItem of this.data.items){
          if(item !== modelItem && modelItem.expanded){
            modelItem.expanded = false;
          }
        }
      }
    }
    item.expanded = !item.expanded;
  }

  getActiveClass(item: ISidebarData): string {
    return item.expanded && this.router.url.includes(item.routeLink)
      ? 'active-sublevel'
      : '';
  }

}
