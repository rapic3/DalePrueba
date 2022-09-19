export interface ResponseApiFilterPaginate {
  state: boolean;
  message: any[];
  information: FilterPaginateResult;
  type: number;
}

export interface FilterPaginateResult {
  totalItems: number;
  pageSize: number;
  pageLenght?: number;
  items?: any[];
}
