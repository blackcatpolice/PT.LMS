export default [{
    path: '/user',
    layout: false,
    routes: [
        {
            name: 'login',
            path: '/user/login',
            component: './user/login',
            access: 'anonymous'
        },
        {
            name: 'register',
            path: '/user/register',
            component: './user/register',
            access: 'anonymous',
        },
    ],
},
{
    path: '/Dashboard',
    name: 'Dashboard',
    icon: 'DashboardOutlined', 
    routes: [
        {
            path: '/Dashboard',
            redirect: '/Dashboard/Analysis',
        },
        {
            name: 'Analysis',
            path: '/Dashboard/Analysis',
            component: './Dashboard/Analysis',
            icon: 'PieChartOutlined',
        },
        {
            name: 'Monitor',
            path: '/Dashboard/Monitor',
            component: './Dashboard/Monitor',
            icon: 'MonitorOutlined',
            access: 'anonymous',
        },
        {
            name: 'Workbench',
            path: '/Dashboard/Workbench',
            component: './Dashboard/Workbench',
            icon: 'GroupOutlined',
            access: 'anonymous',
        },
    ]
},]