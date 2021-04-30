export default [{
    path: '/Course/',
    name: 'Course',
    icon: 'BookOutlined',
    access: 'canAdmin',  
    routes: [
        {
            path: '/Course',
            redirect: '/Course/Article',
        },
        {
            name: 'Category',
            path: '/Course/category',
            icon:'UnorderedListOutlined',
            component: './Course/category',
        },
        {
            name: 'Course ',
            path: '/Course/Article',
            icon:'SnippetsOutlined',
            component: './Course/Article',
            exact: true,
        },
        {
            name: 'Course Edit',
            path: '/Course/edit',
            component: './Course/Article/edit.tsx',
            hideInMenu: true,
        },
        {
            name: 'Lesson',
            path: '/Course/edit/lesson',
            component: './Course/Article/lesson.tsx',
            hideInMenu: true,
        },
        {
            name: 'Section',
            path: '/Course/edit/lesson/section',
            component: './Course/Article/section.tsx',
            hideInMenu: true
        },
    ]
}]