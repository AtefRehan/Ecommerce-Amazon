import { Component } from '@angular/core';
import { Product } from 'src/interfaces/product';
import { ProductsService } from 'src/service/products.service';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-delet',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './delete.component.html',
  styleUrls: ['./delete.component.css']
})
export class DeletComponent {
  product: Product | undefined;
  productId: number | undefined;

constructor(private httpproduct:ProductsService){}
  onDelet() {
    if (this.productId) {
      this.httpproduct.deleteProduct(this.productId).subscribe(
        () => {
          confirm(`Are you sure to delet this product ?`)
          console.log('Product deleted successfully');
        },
        error => {
          alert("This product is unavailable !")
          console.error('Error deleting product:', error);
        }
      );
    } else {
      console.error('Product ID is undefined');
    }
  }

}
