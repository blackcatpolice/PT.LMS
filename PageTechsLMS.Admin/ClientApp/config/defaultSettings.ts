import { Settings as LayoutSettings } from '@ant-design/pro-layout';

export default {
  navTheme: 'light',
  // 拂晓蓝
  primaryColor: '#1890ff',
  layout: 'mix',
  splitMenus: true,
  contentWidth: 'Fluid',
  fixedHeader: false,
  fixSiderbar: false,

  colorWeak: false,
  menu: {
    locale: false,
  },
  title: 'PTLMS',
  pwa: false,
  iconfontUrl: '',
} as LayoutSettings & {
  pwa: boolean;
};
