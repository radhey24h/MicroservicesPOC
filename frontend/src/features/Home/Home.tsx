import { useCallback, useState } from "react";
import type { Container, Engine } from "tsparticles-engine";
import Particles from "react-tsparticles";
import { loadSlim } from "tsparticles-slim";
import ITImages from '../../assets/img/hero-it-img.png';
import Register from "../User/Register/Register";
import './Home.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import '@fortawesome/fontawesome-free/css/all.css';
import Login from "../User/Login/Register";
import Header from "../../components/Header/Header";
import Footer from "../../components/Footer/Footer";

const Home = () => {
    const [loginOrRegister, setLoginOrRegister] = useState(false);

    const toggleView = () => {
        setLoginOrRegister(!loginOrRegister);
    };

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


                <div className="row text-center mb-4">
                    <div className="col-12 d-flex justify-content-center">
                        <div className="elementor-heading">
                            <h2 className="fadeInDown" data-text="Transform Learning into Employment...!">
                                Transform Learning into Employment...!
                            </h2>
                        </div>
                    </div>
                </div>
                <br /><br />
                <div className="row">
                    <div className="col-md-7 text-center mb-4">
                        <img src={ITImages} alt="IT Trainings" className="img-fluid" style={{ height: '60%', width: '50%' }} />
                        <h5 className="elementor-fadeInDown">
                            We are offering free training sessions on AWS, Azure, React-JS and dotnet core.
                            <br></br>Sessions are held daily from 8:00 to 10:00 PM.
                        </h5>
                    </div>
                    <div className="col-md-5 text-center mb-4">
                        <div className="form-background">
                            <h5 className="elementor-fadeInDown">Please enter your details here! &nbsp;
                                <a style={{ cursor: 'pointer' }} onClick={toggleView} >
                                    {loginOrRegister ? 'Register' : 'Login'}
                                </a></h5>
                            <br />
                            {loginOrRegister ? <Login /> : <Register />}
                        </div>
                    </div>
                </div>

                <div className="row">
                    <Footer />
                </div>
            </div>

        </>
    );
};

export default Home;