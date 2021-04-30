import React, { useEffect, useState } from 'react';
import { Tabs, Form, Radio, Input, Button, Card } from 'antd';
import { PageContainer, FooterToolbar } from '@ant-design/pro-layout';
import DynamicForm from '@/components/DynamicForm/DynamicForm'
import { FilebaseInfo, Client, FilebaseSetting } from '@/apis/API'
import { ModelType, ControlType } from '@/components/DynamicForm/DynamicForm.d';
import SetEditingForm from './components/settingForm'
import BasicSetEditingForm from './components/basicSettingForm'

const fbClient = new Client()

const filebase: React.FC<{}> = ({ }) => {
    const { TabPane } = Tabs

    return (
        <PageContainer>
            <Card>
                <Tabs defaultActiveKey="1" >
                    <TabPane tab="Basic" key="1">
                        <BasicSetEditingForm></BasicSetEditingForm>
                    </TabPane>
                    <TabPane tab="Tab 2" key="2">
                        <SetEditingForm></SetEditingForm>

                    </TabPane>
                    <TabPane tab="Tab 3" key="3">
                        <SetEditingForm></SetEditingForm>

                    </TabPane>
                </Tabs>
            </Card>
        </PageContainer>)
}

export default filebase