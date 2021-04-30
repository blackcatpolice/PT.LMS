import React, { useState } from 'react'
import { history } from 'umi'
import { PageContainer } from '@ant-design/pro-layout'
import { Card, Row, Col, List, Skeleton, Button } from 'antd'
import EditForm from './components/editForm'



const page: React.FC<{ courseId: any }> = (props: any) => {
    const [initLoading, setLoading] = useState();
    const [list, setList] = useState();
    const { courseId } = props

    return (
        <>
            <Button type="primary" onClick={async () => {
                history.push('/course/edit/lesson')
            }}>新增lesson</Button>
            <List
                className="demo-loadmore-list"
                loading={initLoading}
                itemLayout="horizontal"
                dataSource={list}
                renderItem={(item: any) => (
                    <List.Item
                        actions={[<a key="list-loadmore-edit">edit</a>, <a key="list-loadmore-more">more</a>]}
                    >
                        <Skeleton avatar title={false} loading={item.loading} active>
                            <List.Item.Meta
                                title={<a href="https://ant.design">{item.name.last}</a>}
                                description="Ant Design, a design language for background applications, is refined by Ant UED Team"
                            />
                            <div>content</div>
                        </Skeleton>
                    </List.Item>
                )}
            />
        </>
    );
}

export default page;