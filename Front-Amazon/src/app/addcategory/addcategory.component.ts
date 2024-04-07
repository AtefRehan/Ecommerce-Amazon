import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CategoryService } from 'src/service/category.service';
interface Category {
  categoryName: string;
}

@Component({
  selector: 'app-addcategory',
  templateUrl: './addcategory.component.html',
  styleUrls: ['./addcategory.component.css']
})


export class AddcategoryComponent implements OnInit {
  categories: Category[] = [];
  newCategoryName!: string;
  categoryIdToDelete: any;

  constructor(private http: HttpClient,private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.fetchCategories();
  }

  fetchCategories(): void {
    this.http.get<Category[]>('http://localhost:5189/api/Category')
      .subscribe(
        (data: Category[]) => {
          this.categories = data;
        },
        (error) => {
          console.error('Error fetching categories:', error);
        }
      );
  }

  addCategory(): void {
    if (!this.newCategoryName) {
      console.error('Category name cannot be empty.');
      return;
    }

    const newCategory: Category = { categoryName: this.newCategoryName };

    this.http.post<Category>('http://localhost:5189/api/Category', newCategory)
      .subscribe(
        (data: Category) => {
          console.log('Category added successfully:', data);
          this.fetchCategories(); // Refresh the categories list
          this.newCategoryName = ''; // Clear the input field
        },
        (error) => {
          console.error('Error adding category:', error);
        }
      );
  }

  deleteCategory(): void {
    if (!this.categoryIdToDelete) {
      alert('Please enter the category ID.');
      return;
    }

    this.categoryService.deleteCategory(this.categoryIdToDelete).subscribe(
      () => {
        console.log('Category deleted successfully');
      },
      error => {
        console.error('Error deleting category:', error);
      }
    );
  }
}
