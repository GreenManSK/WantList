import { Component, ContentChild, Input, OnChanges, OnInit, SimpleChanges, TemplateRef } from '@angular/core';
import { Column } from './Column';
import { faSortAmountDownAlt as sortAscIcon, faSortAmountUp as sortDescIcon } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-list-table',
  templateUrl: './list-table.component.html',
  styleUrls: ['./list-table.component.sass']
})
export class ListTableComponent implements OnInit, OnChanges {

  @ContentChild(TemplateRef, {static: false})
  columnTemplate: TemplateRef<any>;

  @Input() data: any[];
  @Input() columns: Column[];
  @Input() defaultSortKey: string;
  @Input() defaultIsAsc = true;

  public sortAscIcon = sortAscIcon;
  public sortDescIcon = sortDescIcon;

  public sortKey: string;
  public isAsc: boolean;

  constructor() {
  }

  ngOnInit(): void {
    if (!this.defaultSortKey) {
      this.defaultSortKey = this.columns[0].key;
    }
    this.sortKey = this.defaultSortKey;
    this.isAsc = this.defaultIsAsc;
    this.sortData();
  }

  ngOnChanges( changes: SimpleChanges ) {
    this.sortData();
  }

  public changeSort( key: string ): boolean {
    if (key === this.sortKey) {
      this.isAsc = !this.isAsc;
    } else {
      this.sortKey = key;
      this.isAsc = true;
    }
    this.sortData();
    return false;
  }

  private sortData(): void {
    this.data.sort(this.sorter.bind(this));
    if (!this.isAsc) {
      this.data.reverse();
    }
  }

  private sorter( a: any, b: any ) {
    const valA = a[this.sortKey];
    const valB = b[this.sortKey];
    if (typeof valB === 'string') {
      return valA.localeCompare(valB);
    }
    if (valB instanceof Date) {
      return valA.getTime() - valB.getTime();
    }
    return valA - valB;
  }

}
