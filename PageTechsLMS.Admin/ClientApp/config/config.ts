// https://umijs.org/config/
import { defineConfig } from 'umi';
import defaultSettings from './defaultSettings';
import proxy from './proxy';
import routes from './routes';

const { REACT_APP_ENV } = process.env;

export default defineConfig({
  esbuild: {},
  define: {
    "process.env.apiUrl": 'https://localhost:44366'
  },
  hash: true,
  antd: {},
  dva: {
    hmr: true,
  },
  layout: {
    name: 'PTLMS',
    logo: '/logo.png',
    locale: true,
    siderWidth: 208,
  },
  locale: {
    // default zh-CN
    default: 'zh-CN',
    // default true, when it is true, will use `navigator.language` overwrite default
    antd: true,
    baseNavigator: true,
  },
  dynamicImport: {
    loading: '@/components/PageLoading/index',
  },
  targets: {
    ie: 11,
  },
  // umi routes: https://umijs.org/docs/routing
  routes: [
    ...routes,
    {
      path: '/',
      redirect: '/Dashboard',
    },
    {
      menuRender: false,
      component: './404',
    },
  ],
  // Theme for antd: https://ant.design/docs/react/customize-theme-cn
  theme: {
    //...darkTheme,
    'primary-color': defaultSettings.primaryColor,
  },
  // @ts-ignore
  title: false,
  ignoreMomentLocale: true,
  //proxy: proxy[REACT_APP_ENV || 'dev'],
  manifest: {
    basePath: '/management',
  },
  base: '/management',
});
