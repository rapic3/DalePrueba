import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { FilterPaginate } from '../../models/filter_paginate.model';
import { FilterPaginateResult } from '../../models/response_api_filter_paginate.model';

@Component({
  selector: 'app-paginator-app',
  templateUrl: './paginator-app.component.html',
  styleUrls: ['./paginator-app.component.scss'],
})
export class PaginatorAppComponent implements OnInit {
  @Input() paginate: FilterPaginateResult | undefined;
  @Output() goToPage = new EventEmitter<number>();
  filter: FilterPaginate = { page: 1 };

  constructor() {}

  ngOnInit(): void {
    //console.log(this.paginate);
    //console.log(this.paginate);
  }

  onPageChange() {
    this.goToPage.emit(this.filter.page);
  }
}
