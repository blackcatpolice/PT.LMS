import React, { useState, useEffect } from 'react'
import { PageContainer } from '@ant-design/pro-layout'
import { Card, Row, Col } from 'antd'
import EditForm from './components/editForm'
import LessonList from './components/editLessonList'



const page: React.FC<{}> = (props: any) => {
    const [courseId, setCourseId] = useState<any>();

    useEffect(() => {
        setCourseId(1)
    }, [])
    return (
        <PageContainer> 
            <Row>
                <Col span={courseId ? 12 : 24}>
                    <Card>
                        <EditForm></EditForm>
                    </Card>
                </Col>
                {courseId && <Col span={12}>
                    <Card>
                        <LessonList courseId="123"></LessonList>
                    </Card>
                </Col>}
            </Row>
        </PageContainer>
    );
}

export default page;