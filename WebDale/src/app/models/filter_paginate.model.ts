export interface FilterPaginate {
  id?: number;
  search?: string;
  sortBy?: string;
  isSortAscending?: boolean;
  page: number;
  pageSize?: number;
  sortProduct?: string;
  filterStatus?: string[];
  filterDateFrom?: string;
  filterDateTo?: string;
}
