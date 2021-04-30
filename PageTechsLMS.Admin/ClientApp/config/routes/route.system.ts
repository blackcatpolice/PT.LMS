export default [
    {
        name: "Setting",
        path: '/Setting',
        icon: 'InfoCircleOutlined',
        routes: [
            {
                path: '/Setting',
                redirect: '/Setting/SettingGroup'
            },
            {
                name: "Setting",
                path: '/Setting/SettingGroup',
                icon: 'SettingOutlined',
                access: 'canAdmin',
                routes: [
                    {
                        path: '/Setting/SettingGroup',
                        redirect: '/Setting/SettingGroup/Index',
                    },
                    {
                        path: '/Setting/SettingGroup/Index',
                        name: 'SiteSetting',
                        component: './Setting/Index',
                    },
                    {
                        path: '/Setting/SettingGroup/config',

                        name: 'ConfigSetting',
                        component: './Setting/config',
                    }
                ],
            },
            {
                path: '/Setting/UserGroup/userInfo',
                name: 'UserInfo',
                icon: 'UserOutlined',
                access: 'checkPermission',
                routes: [
                    {
                        name: 'Info',
                        path: '/Setting/UserGroup/userInfo/info',
                        component: './userInfo/info',
                        exact: true,
                    },
                    {
                        name: 'Changepwd',
                        path: '/Setting/UserGroup/userInfo/changepwd',
                        component: './userInfo/changepwd',
                        exact: true,
                    },
                ]
            }
        ]
    }
]