export default [
    {
        path: '/Member',
        name: 'Member',
        icon: 'UsergroupAddOutlined', 
        routes: [
            {
                path:'/Member',
                redirect: '/Member/Index'
            },
            {
                name: 'MemberList',
                path: '/Member/Index',
                component: './Member/MemberList',
                icon: 'UsergroupAddOutlined',
                access: 'anonymous',
                exact: true,
            }
        ]
    }]