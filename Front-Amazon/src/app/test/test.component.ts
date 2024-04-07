import { Component, TrackByFunction } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from 'src/interfaces/product';
import { AuthService } from 'src/service/auth.service';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})

export class TestComponent {
  products: Product[] = [];
  term: string = '';
  inputData: string = '';

trackByFn: TrackByFunction<Product> = (index, item) => item.productId;

  constructor(private search: AuthService,private router:Router) { }

  ngOnInit(): void {
    this.search.getallproduct().subscribe((Response: any) => {
      console.log('products', Response);
      this.products = Response;
    });
  }

  onSearch(): void {
    if (this.term.trim() !== '') {
      this.search.searchProducts(this.term).subscribe((data) => {
        console.log(data);
        this.products = data;
      });
    } else {
      this.search.getallproduct().subscribe((Response: any) => {
        console.log('products', Response);
        this.products = Response;
      });
    }
  }
  printInputData() {
    this.router.navigate(['/asd'], { queryParams: { data: this.inputData } });
  }
}
