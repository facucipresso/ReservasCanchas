import { definePreset } from '@primeuix/themes';
import Aura from '@primeuix/themes/aura';

const MyPreset = definePreset(Aura, {
    semantic: {
        primary: {
            50: '#fafafa',
            100: '#e5e5e5',
            200: '#cfcfcf',
            300: '#a8a8a8',
            400: '#737373',
            500: '#000000',   // color principal MUY oscuro
            600: '#0a0a0a',
            700: '#141414',
            800: '#1f1f1f',
            900: '#262626'
        },
        surface: {
            0: '#000000',   // fondo
            50: '#111111',
            100: '#1a1a1a',
            200: '#262626'
        },
        content: {
            text: '#ffffff',
            textMuted: '#cccccc',
        }
    }
});

export default MyPreset;