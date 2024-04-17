import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from 'src/service/auth.service';

@Component({
  selector: 'app-addsupplier',
  templateUrl: './addsupplier.component.html',
  styleUrls: ['./addsupplier.component.css']
})

export class AddsupplierComponent {
  supplier = {
    supplierName: '',
    email: '',
    address: '',
    phoneNumber: ''
  };
  supplierIdToDelete: any;

  constructor(private http: HttpClient,private supplierService:AuthService) {}

  submitSupplierForm(): void {
    this.http.post<any>('http://localhost:5189/api/Supplier', this.supplier)
      .subscribe(
        (response) => {
          alert("Supplier Add Successfuly")
          console.log('Supplier added successfully:', response);

        },
        (error) => {
          console.error('Error adding supplier:', error);
        }
      );
  }
  deleteSupplier(): void {
    if (!this.supplierIdToDelete) {
      alert('Please enter the supplier ID.');
      return;
    }

    this.supplierService.deleteSupplier(this.supplierIdToDelete).subscribe(
      () => {
        console.log('Supplier deleted successfully');
      },
      error => {
        console.error('Error deleting supplier:', error);
      }
    );
  }

}
