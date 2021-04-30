export default [{
    name: "Finance",
    path: '/finance',
    icon: 'MoneyCollectOutlined',
    access: 'canAdmin',
    component: './finance/',
    layout: {
        hideMenu: false,
        hideNav: false,
        hideFooter: false,
    }, 
    // 不展示顶栏
    //headerRender: false,
    // 不展示页脚
    footerRender: false,
    // 不展示菜单
    menuRender: false,
    // 不展示菜单顶栏
    //menuHeaderRender: false,
}]