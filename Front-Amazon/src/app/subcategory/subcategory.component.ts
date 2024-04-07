import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from 'src/service/category.service';

interface SubCategory {
  subCategoryId: number;
  subCategoryName: string;
}

interface Category {
  categoryId: number;
  categoryName: string;
  subCategories: SubCategory[];
}

@Component({
  selector: 'app-subcategory',
  templateUrl: './subcategory.component.html',
  styleUrls: ['./subcategory.component.css']
})
export class SubcategoryComponent implements OnInit {
  subCategories: SubCategory[] = [];
  categoryId!: number;
subcategoryId!: number;

  constructor(private route: ActivatedRoute, private categoryService: CategoryService,private router:Router) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const categoryIdString = params.get('categoryId');
      const categoryId = categoryIdString ? +categoryIdString : 0;
            if (categoryId) {
              console.log(categoryId)
        this.categoryService.getCategories().subscribe(
          (categories: Category[]) => {
            const category = categories.find(cat => cat.categoryId === categoryId);
            if (category) {
              this.subCategories = category.subCategories;
            } else {
              console.error('Category not found.');
            }
          },
          (error) => {
            console.error('Error fetching categories:', error);
          }
        );
      } else {
        console.error('Category ID is null or undefined.');
      }
    });
  }

  viewProductsInSubcategories(subcategoryId: number): void {
    this.router.navigate(['/products', subcategoryId]);
  }
}
