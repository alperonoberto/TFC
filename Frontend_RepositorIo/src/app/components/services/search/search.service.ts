import { EventEmitter, Injectable, Output } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  constructor() { }
  
  @Output() userSearched: EventEmitter<any> = new EventEmitter();
}
