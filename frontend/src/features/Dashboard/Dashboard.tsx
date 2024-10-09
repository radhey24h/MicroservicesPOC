import { useCallback, useState } from "react";
import type { Container, Engine } from "tsparticles-engine";
import Particles from "react-tsparticles";
import { loadSlim } from "tsparticles-slim";
import logo from '../../assets/img/logo.png';
import './Dashboard.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import '@fortawesome/fontawesome-free/css/all.css';
import Header from "../../components/Header/Header";
import Footer from "../../components/Footer/Footer";

const Dashboard = () => {
    const [alreadyLogin, setAlreadyLogin] = useState(false);

    const particlesInit = useCallback(async (engine: Engine) => {

        await loadSlim(engine);
    }, []);

    const particlesLoaded = useCallback(async (container: Container | undefined) => {
        await console.log(container);
    }, []);
    return (
        <>
            <Particles
                id="tsparticles"
                init={particlesInit}
                loaded={particlesLoaded}
                options={{
                    background: {
                        color: {
                            value: "#207cca",
                        },
                    },
                    fpsLimit: 120,
                    particles: {
                        color: {
                            value: "#ffffff",
                        },
                        links: {
                            color: "#ffffff",
                            distance: 150,
                            enable: true,
                            opacity: 0.5,
                            width: 1,
                        },
                        move: {
                            direction: "none",
                            enable: true,
                            outModes: {
                                default: "bounce",
                            },
                            random: true,
                            speed: 2,
                            straight: true,
                        },
                        number: {
                            value: 150,
                            density: {
                                enable: true,
                                value_area: 1000,
                            }
                        },
                        shape: {
                            type: "circle",
                        },
                        size: {
                            value: 1,
                            random: true,
                        },
                    },
                    detectRetina: true,
                }}
            />

            <div className="container" style={{ position: 'relative' }}>
                <Header />
                <div className="row form-background">
                    <div className="dashoard-continer">

                    </div>
                </div>
                <div className="row">
                    <Footer />
                </div>
            </div>

        </>
    );
};

export default Dashboard;