const request = require('supertest');
const app = require('../app');

describe('HospitalController Integration Tests', () => {
    it('should return all hospitals with addresses (GET /api/Hospital)', async () => {
        const response = await request(app).get('/api/Hospital');
        expect(response.statusCode).toBe(200);
        expect(Array.isArray(response.body)).toBeTruthy();

        if (response.body.length > 0) {
            expect(response.body[0]).toHaveProperty('hospital_ID');
            expect(response.body[0]).toHaveProperty('address');
        }
    });

    it('should return a specific hospital by ID (GET /api/Hospital/:id)', async () => {
        const id = 1;
        const response = await request(app).get(`/api/Hospital/${id}`);
        expect(response.statusCode).toBe(200);

        const hospital = response.body;
        expect(hospital).toHaveProperty('hospital_ID', id);
        expect(hospital).toHaveProperty('address');
    });

    it('should return 404 for non-existent hospital (GET /api/Hospital/:id)', async () => {
        const invalidId = 9999;
        const response = await request(app).get(`/api/Hospital/${invalidId}`);
        expect(response.statusCode).toBe(404);
    });
});
