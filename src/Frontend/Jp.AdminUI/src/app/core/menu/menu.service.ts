import { Injectable } from '@angular/core';

import { MenuItem } from './menu';

@Injectable()
export class MenuService {

    menuItems: Array<any>;

    constructor() {
        this.menuItems = [];
    }

    addMenu(items: Array<MenuItem>) {
        items.forEach((item) => {
            this.menuItems.push(item);
        });
    }

    getMenu() {
        return this.menuItems;
    }

}
