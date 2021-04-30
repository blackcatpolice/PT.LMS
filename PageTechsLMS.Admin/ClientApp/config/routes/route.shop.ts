export default [{
    path: '/Shop',
    name: 'Shop',
    icon: 'BookOutlined',
    access: 'canAdmin', 
    routes: [
        {
            name: 'ShopCategory',
            path: '/Shop/Category',
            component: './Shop/Category',
        },
        {
            name: 'ShopGoods',
            path: '/Shop/goods',
            component: './Shop/goods',
        }, 
    ]
}]