import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from 'src/interfaces/product';
import { CategoryService } from 'src/service/category.service';
import { GlobalstateService } from 'src/service/globalstate.service';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent implements OnInit {
keyword!: string;

  constructor(private router:Router,private categoryService:CategoryService) { }

  items: Product[] | undefined

  searchedItem: string=''

  cart: Product[] =[]

  headerSwitch:boolean=true;
  subcategories!: any[];
  categoryId: number = 1;

  itemImages = ['assets/item1.jpg', 'assets/item2.jpg', 'assets/item3.jpg','assets/item4.jpg', 'assets/item5.jpg', 'assets/item6.jpg', 'assets/item7.jpg', 'assets/item3.jpg','assets/item4.jpg', 'assets/item5.jpg', 'assets/item6.jpg', 'assets/item7.jpg', 'assets/item3.jpg','assets/item4.jpg', 'assets/item5.jpg', 'assets/item6.jpg', 'assets/item7.jpg',]

  categories: any[] = [];


  ngOnInit(): void {
    this.categoryService.getCategories().subscribe(
      (data) => {
        this.categories = data;
      },
      (error) => {
        console.error('Error fetching categories:', error);
      }
    );
  }

  navigateToSubcategories(categoryId: number): void {
    this.router.navigate(['/subcategories', categoryId]);
  }
}


