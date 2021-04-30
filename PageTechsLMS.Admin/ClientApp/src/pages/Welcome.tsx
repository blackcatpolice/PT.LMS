import React from 'react';
import { PageContainer } from '@ant-design/pro-layout';
import Charts from 'ant-design-pro/lib/Charts';
import { Card, Alert, Typography } from 'antd';
import styles from './Welcome.less';

const CodePreview: React.FC<{}> = ({ children }) => (
  <pre className={styles.pre}>
    <code>
      <Typography.Text copyable>{children}</Typography.Text>
    </code>
  </pre>
);

export default (): React.ReactNode => (
  <PageContainer>
    <Card>
      <Alert
        message="欢迎使用知页科技,后台管理系统"
        type="success"
        showIcon
        banner
        style={{
          margin: -12,
          marginBottom: 24,
        }}
      />

    </Card>
  </PageContainer>
);
