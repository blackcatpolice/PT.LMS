import React, { useState, useEffect } from 'react'
import { Steps, Button, message } from 'antd'
import Footer from '@/components/Footer';

import styles from './style.less';

const { Step } = Steps
const steps = [
    {
        title: '账号注册',
        content: (<><div>test</div></>),
    },
    {
        title: '认证',
        content: 'Second-content',
    },
    {
        title: '完成',
        content: 'Last-content',
    },
];
const register: React.FC<{}> = () => {
    const [current, setCurrent] = useState(0)
    const next = () => {
        setCurrent(current + 1);
    };

    const prev = () => {
        setCurrent(current - 1);
    };
    return (<>
        <div className={styles.container}>
            <div className={styles.content}>
                <div className={styles.top}>
                </div>
                <div className={styles.main}>
                    <Steps current={current}>
                        {steps.map(item => (
                            <Step key={item.title} title={item.title} />
                        ))}
                    </Steps>
                    <div className="steps-content">{steps[current].content}</div>
                    <div className="steps-action">
                        {current < steps.length - 1 && (
                            <Button type="primary" onClick={() => next()}>
                                Next
                            </Button>
                        )}
                        {current === steps.length - 1 && (
                            <Button type="primary" onClick={() => message.success('Processing complete!')}>
                                Done
                            </Button>
                        )}
                        {current > 0 && (
                            <Button style={{ margin: '0 8px' }} onClick={() => prev()}>
                                Previous
                            </Button>
                        )}
                    </div>
                </div>
            </div>

            <Footer />
        </div>
    </>)
}

export default register