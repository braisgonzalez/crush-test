package com.example.demo.application.health;

import com.example.demo.domain.health.HealthService;
import org.springframework.stereotype.Service;

@Service
public class HealthQueryServiceImpl implements HealthQueryService {
    private final HealthService healthService = new HealthService();

    @Override
    public String health() {
        return healthService.check();
    }
}
