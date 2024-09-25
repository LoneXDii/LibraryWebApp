import './App.css';
import Header from './components/TestReact/Header/Header';
import { list, bContent } from './TestData/data';
import { useState} from 'react';
import List from './components/TestReact/List/List';
import ButtonSection from './components/TestReact/ButtonSection/ButonSection';
import IntroSection from './components/TestReact/IntroSection';
import TabsSection from './components/TestReact/TabsSection';
import FeedbackSection from './components/TestReact/FeedbackSection';
import HttpSection from './components/TestReact/httpSection';

function TestApp() {
    const [now, setNow] = useState(new Date())
    const [tab, setTab] = useState('feedback')

    setInterval(() => setNow(new Date), 1000)

    //console.log('App component render')

    return (
        <>
            <Header/>
            <main>
                <IntroSection/>
                <TabsSection active={tab} onChage={(current) => setTab(current)}/>

                {tab === 'main' && (
                    <>
                        <List list={list} />
                        <ButtonSection />
                    </>
                )}
                {tab === 'feedback' && (
                    <>
                        <FeedbackSection/>
                    </>
                )}
                {tab === 'http' && (
                    <>
                        <HttpSection/>
                    </>
                )}
            </main>
            <span>Время сейчас: {now.toLocaleTimeString()}</span>
        </>
    );
}

export default TestApp;
