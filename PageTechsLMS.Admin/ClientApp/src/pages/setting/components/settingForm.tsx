import React, { useState } from 'react'
import { PageContainer } from '@ant-design/pro-layout'
import { Form, Input, Button } from 'antd';
import { InfoCircleOutlined } from '@ant-design/icons';

type RequiredMark = boolean | 'optional';

const { TextArea } = Input;

const page: React.FC<{}> = (props: any) => {
    const [form] = Form.useForm();
    // const [requiredMark, setRequiredMarkType] = useState<RequiredMark>('optional');

    // const onRequiredTypeChange = ({ requiredMarkValue }: { requiredMarkValue: RequiredMark }) => {
    //     setRequiredMarkType(requiredMarkValue);
    // };

    const saveSetting = () => {

    }

    const getSetting = () => {

    }

    return (
        <Form
            form={form}
            layout="vertical"
            requiredMark={"optional"}
            onFinish={saveSetting}
        >
            <Form.Item label="站点名" tooltip="站点的名称" name="siteName">
                <Input placeholder="Site name" />
            </Form.Item>
            <Form.Item
                label="关键字"
                name="kyes"
                tooltip="SEO 关键字"
            >
                <Input placeholder="" />
            </Form.Item>
            <Form.Item
                label="描述"
                tooltip="SEO 描述"
                name="description"
            >
                <Input placeholder="" />
            </Form.Item>
            <Form.Item
                label="站点底部信息"
                tooltip="底部copyright 内容"
                name="footerInfo"
            >
                <TextArea placeholder="" />
            </Form.Item>
            <Form.Item
                label="站点信息脚本"
                tooltip="站长之家 ，广告外联等信息脚本"
                name="footerScript"
            >
                <TextArea placeholder="" />
            </Form.Item>
            <Form.Item >
                <Button type="primary">保存</Button>
            </Form.Item>
        </Form>
    );
}

export default page;