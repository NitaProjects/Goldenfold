import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup, FormControl, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-search-box',
  templateUrl: './search-box.component.html',
  styleUrl: './search-box.component.css',
})
export class SearchBoxComponent {
  @Input() searchOptions: { label: string; value: string }[] = [];
  @Input() defaultSearchType: string = '';
  @Input() placeholder: string = 'Escribe para buscar...';
  @Output() search = new EventEmitter<{ type: string; term: string }>();

  searchForm = new FormGroup({
    searchType: new FormControl(''),
    searchTerm: new FormControl(''),
  });

  ngOnInit(): void {
    this.searchForm
      .get('searchType')
      ?.setValue(this.defaultSearchType || this.searchOptions[0].value);
  }

  onSearch(): void {
    const searchType = this.searchForm.get('searchType')?.value ?? '';
    const searchTerm = this.searchForm.get('searchTerm')?.value ?? '';
    this.search.emit({ type: searchType, term: searchTerm });
  }
}
