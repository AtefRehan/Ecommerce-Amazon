import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/service/category.service';

interface Subcategory {
  subCategoryName: string;
  categoryId: number;
}


@Component({
  selector: 'app-addsubcategory',
  templateUrl: './addsubcategory.component.html',
  styleUrls: ['./addsubcategory.component.css']
})
export class AddsubcategoryComponent implements OnInit {
  Subcategories: Subcategory[] = [];
  newSubcategoryName!: string ;
  selectedCategoryId: number| undefined;
subcategory: any;
  subcategoryIdToDelete: any;
  selectedSubCategoryId: any;

  constructor(private http: HttpClient,private subcategoryService:CategoryService) { }

  ngOnInit(): void {
    this.fetchSubcategories();
  }

  fetchSubcategories(): void {
    this.http.get<Subcategory[]>('http://localhost:5189/api/SubCategory')
      .subscribe(
        (data: Subcategory[]) => {
          this.Subcategories = data;
        },
        (error) => {
          console.error('Error fetching subcategories:', error);
        }
      );
  }

  addSubcategory(): void {
    if (!this.newSubcategoryName || !this.selectedCategoryId) {
      console.error('Subcategory name and category must be selected.');
      return;
    }
    let newSubcategory: Subcategory = {
      subCategoryName: this.newSubcategoryName,
      categoryId: this.selectedCategoryId
    };

    this.http.post<Subcategory>('http://localhost:5189/api/SubCategory', newSubcategory)
      .subscribe(
        (data:Subcategory) => {
          console.log('Subcategory added successfully:', data);
          this.fetchSubcategories();
          this.newSubcategoryName = '';
          this.selectedCategoryId = undefined;
        },
        (error) => {
          console.error('Error adding subcategory:', error);
        }
      );
  }

  deleteSubcategory(): void {
    if (!this.subcategoryIdToDelete) {
      alert('Please enter the subcategory ID.');
      return;
    }

    this.subcategoryService.deleteSubcategory(this.subcategoryIdToDelete).subscribe(
      () => {
        console.log('Subcategory deleted successfully');
      },
      error => {
        console.error('Error deleting subcategory:', error);
      }
    );
  }
}
