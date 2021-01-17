export class Column {
  public key: string;
  public title: string;
  public sortable: boolean;
  public class: string;

  constructor( key: string, title: string, sortable: boolean, className: string = '') {
    this.key = key;
    this.title = title;
    this.sortable = sortable;
    this.class = className;
  }
}
