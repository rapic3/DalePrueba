import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  isCollapsed = false;


  ngOnInit(): void {
  }

  toggle() {
    const body = document.getElementsByTagName('body')[0];

    if (!this.isCollapsed) {
      body.classList.add('sb-sidenav-toggled');
      this.isCollapsed = !this.isCollapsed;
    } else {
      this.isCollapsed = !this.isCollapsed;
      body.classList.remove('sb-sidenav-toggled');
    }
  }
}
